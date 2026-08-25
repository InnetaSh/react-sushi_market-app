import React, { useState } from 'react';
import { useNavigate, Navigate } from 'react-router-dom';
import { Form, Input, Button, Checkbox, message } from 'antd';
import { useTranslation } from 'react-i18next';
import { GoogleLogin } from '@react-oauth/google';
import { observer } from 'mobx-react-lite';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import AuthStore from "@stores/AuthStore";
import UserApi from "@api/UserApi";
import styles from './LoginSection.module.scss';

const LoginSection: React.FC = () => {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const [isRegistering, setIsRegistering] = useState(false);
    const [form] = Form.useForm();

   const redirectAfterLogin = (userData?: any) => {
        const storeUser = AuthStore.user as any;
        const roles = userData?.roles || storeUser?.roles || [];
        
        if (roles.includes('MainAdministrator')) {
            navigate('/admin');
        } else {
            navigate('/');
        }
    };

    if (AuthStore.isLoggedIn) {
        return <Navigate to={AuthStore.isAdmin ? '/admin' : '/'} replace />;
    }

    const handleFinish = async (values: any) => {
        try {
            setIsLoading(true);

            if (isRegistering) {
                await UserApi.register({
                    name: values.name,
                    email: values.login,
                    password: values.password,
                });
                message.success(t('AUTH.SUCCESS_REGISTER'));

                setIsRegistering(false);
                setIsLoading(false);
                form.resetFields();
                return;
            }

            const response = await UserApi.login({
                email: values.login,
                password: values.password,
            });

            AuthStore.setUserLoginResponse(response);
            message.success(t('AUTH.SUCCESS_LOGIN'));

            redirectAfterLogin(response?.user || response);
        } catch (error) {
            console.error(error);
            message.error(isRegistering ? t('AUTH.ERROR_REGISTER_FAIL') : t('AUTH.ERROR_LOGIN_FAIL'));
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <PageSectionLayout
            title={isRegistering ? t('AUTH.TITLE_REGISTER') : t('AUTH.TITLE_LOGIN')}
            description={isRegistering ? t('AUTH.SUBTITLE_REGISTER') : t('PAGE_3_TEXT.DESCRIPTION')}
            breadcrumbs={[
                { label: t('AUTH.BREADCRUMB_HOME'), path: '/' },
                { label: isRegistering ? t('AUTH.BREADCRUMB_REGISTER') : t('AUTH.BREADCRUMB_LOGIN') }
            ]}
        >
            <div className={styles.loginContainer}>
                <div className={styles.loginCard}>
                    <h1 className={styles.loginTitle}>
                        {isRegistering ? t('AUTH.TITLE_REGISTER') : t('AUTH.TITLE_LOGIN')}
                    </h1>
                    <p className={styles.loginSubtitle}>
                        {isRegistering ? t('AUTH.DESC_REGISTER') : t('AUTH.SUBTITLE_LOGIN')}
                    </p>

                    <Form
                        form={form}
                        layout="vertical"
                        onFinish={handleFinish}
                    >
                        {isRegistering && (
                            <Form.Item
                                label={t('AUTH.LABEL_NAME')}
                                name="name"
                                rules={[{ required: true, message: t('AUTH.ERROR_NAME') }]}
                            >
                                <Input maxLength={100} placeholder={t('AUTH.PLACEHOLDER_NAME')} />
                            </Form.Item>
                        )}

                        <Form.Item
                            label={t('AUTH.LABEL_EMAIL')}
                            name="login"
                            rules={[
                                { required: true, message: t('AUTH.ERROR_EMAIL_REQUIRED') },
                                { type: 'email', message: t('AUTH.ERROR_EMAIL_INVALID') }
                            ]}
                        >
                            <Input maxLength={256} placeholder={t('AUTH.PLACEHOLDER_EMAIL')} />
                        </Form.Item>

                        <Form.Item
                            label={t('AUTH.LABEL_PASSWORD')}
                            name="password"
                            rules={[{ required: true, message: t('AUTH.ERROR_PASSWORD') }]}
                        >
                            <Input.Password placeholder={t('AUTH.PLACEHOLDER_PASSWORD')} />
                        </Form.Item>

                        {isRegistering && (
                            <Form.Item
                                label={t('AUTH.LABEL_CONFIRM_PASSWORD')}
                                name="confirmPassword"
                                dependencies={['password']}
                                rules={[
                                    { required: true, message: t('AUTH.ERROR_CONFIRM_REQUIRED') },
                                    ({ getFieldValue }) => ({
                                        validator(_, value) {
                                            if (!value || getFieldValue('password') === value) {
                                                return Promise.resolve();
                                            }
                                            return Promise.reject(new Error(t('AUTH.ERROR_PASSWORDS_MATCH')));
                                        },
                                    }),
                                ]}
                            >
                                <Input.Password placeholder={t('AUTH.PLACEHOLDER_CONFIRM_PASSWORD')} />
                            </Form.Item>
                        )}

                        {!isRegistering && (
                            <div className={styles.loginOptions}>
                                <Button type="link" disabled className={styles.linkButton}>
                                    {t('AUTH.FORGOT_PASSWORD')}
                                </Button>
                                <Checkbox defaultChecked>
                                    {t('AUTH.REMEMBER_ME')}
                                </Checkbox>
                            </div>
                        )}

                        <Button
                            className={styles.loginSubmitBtn}
                            type="primary"
                            htmlType="submit"
                            loading={isLoading}
                            block
                        >
                            {isRegistering ? t('AUTH.BTN_REGISTER') : t('AUTH.BTN_LOGIN')}
                        </Button>

                        <div className={styles.loginRegister}>
                            {isRegistering ? t('AUTH.TEXT_HAS_ACCOUNT') : t('AUTH.TEXT_NO_ACCOUNT')}
                            <Button
                                type="link"
                                onClick={() => {
                                    setIsRegistering(!isRegistering);
                                    form.resetFields();
                                }}
                                className={styles.inlineLink}
                            >
                                {isRegistering ? t('AUTH.BTN_LOGIN') : t('AUTH.BTN_REGISTER')}
                            </Button>
                        </div>

                        {!isRegistering && (
                            <>
                                <div className={styles.loginDivider}>
                                    <span className={styles.dividerLine} />
                                    <span className={styles.dividerText}>{t('AUTH.DIVIDER_OR')}</span>
                                    <span className={styles.dividerLine} />
                                </div>

                                <div className={styles.googleButtonWrapper}>
                                    <GoogleLogin
                                        theme="filled_black"
                                        size="large"
                                        text="signin_with"
                                        shape="rectangular"
                                        width="380"
                                        onSuccess={async (credentialResponse) => {
                                            try {
                                                setIsLoading(true);
                                                const response = await UserApi.googleLogin({
                                                    idToken: credentialResponse.credential as string
                                                });
                                                AuthStore.setUserLoginResponse(response);
                                                redirectAfterLogin(response?.user || response);
                                            } catch (e) {
                                                console.error('Google Auth Error:', e);
                                                message.error(t('AUTH.ERROR_GOOGLE_FAIL'));
                                            } finally {
                                                setIsLoading(false);
                                            }
                                        }}
                                        onError={() => {
                                            message.error(t('AUTH.ERROR_GOOGLE_CANCEL'));
                                        }}
                                    />
                                </div>
                            </>
                        )}
                    </Form>
                </div>
            </div>
        </PageSectionLayout>
    );
};

export default observer(LoginSection);