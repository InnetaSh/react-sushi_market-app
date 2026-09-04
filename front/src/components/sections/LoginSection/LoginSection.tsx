import React, { useState } from 'react';
import { useNavigate, Navigate } from 'react-router-dom';
import { Form, Input, Button, Checkbox, message } from 'antd';
import { useTranslation } from 'react-i18next';
import { GoogleLogin } from '@react-oauth/google';
import { observer } from 'mobx-react-lite';

import PageSectionLayout from '@layout/PageSectionLayout/PageSectionLayout';
import AuthStore from "@stores/AuthStore";
import UserApi from "@/api/userApi";
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
                message.success(t('AUTH.SUCCESS_REGISTER' as any));

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
            message.success(t('AUTH.SUCCESS_LOGIN' as any));

            redirectAfterLogin(response?.user || response);
        } catch (error) {
            console.error(error);
            message.error(isRegistering ? t('AUTH.ERROR_REGISTER_FAIL' as any) : t('AUTH.ERROR_LOGIN_FAIL' as any));
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <PageSectionLayout
            title={isRegistering ? t('AUTH.TITLE_REGISTER' as any) : t('AUTH.TITLE_LOGIN' as any)}
            description={isRegistering ? t('AUTH.SUBTITLE_REGISTER' as any) : t('PAGE_3_TEXT.DESCRIPTION' as any)}
            breadcrumbs={[
                { label: t('AUTH.BREADCRUMB_HOME' as any), path: '/' },
                { label: isRegistering ? t('AUTH.BREADCRUMB_REGISTER' as any) : t('AUTH.BREADCRUMB_LOGIN' as any) }
            ]}
        >
            <div className={styles.loginContainer}>
                <div className={styles.loginCard}>
                    <h1 className={styles.loginTitle}>
                        {isRegistering ? t('AUTH.TITLE_REGISTER' as any) : t('AUTH.TITLE_LOGIN' as any)}
                    </h1>
                    <p className={styles.loginSubtitle}>
                        {isRegistering ? t('AUTH.DESC_REGISTER' as any) : t('AUTH.SUBTITLE_LOGIN' as any)}
                    </p>

                    <Form
                        form={form}
                        layout="vertical"
                        onFinish={handleFinish}
                    >
                        {isRegistering && (
                            <Form.Item
                                label={t('AUTH.LABEL_NAME' as any)}
                                name="name"
                                rules={[{ required: true, message: t('AUTH.ERROR_NAME' as any) }]}
                            >
                                <Input maxLength={100} placeholder={t('AUTH.PLACEHOLDER_NAME' as any)} />
                            </Form.Item>
                        )}

                        <Form.Item
                            label={t('AUTH.LABEL_EMAIL' as any)}
                            name="login"
                            rules={[
                                { required: true, message: t('AUTH.ERROR_EMAIL_REQUIRED' as any) },
                                { type: 'email', message: t('AUTH.ERROR_EMAIL_INVALID' as any) }
                            ]}
                        >
                            <Input maxLength={256} placeholder={t('AUTH.PLACEHOLDER_EMAIL' as any)} />
                        </Form.Item>

                        <Form.Item
                            label={t('AUTH.LABEL_PASSWORD' as any)}
                            name="password"
                            rules={[{ required: true, message: t('AUTH.ERROR_PASSWORD' as any) }]}
                        >
                            <Input.Password placeholder={t('AUTH.PLACEHOLDER_PASSWORD' as any)} />
                        </Form.Item>

                        {isRegistering && (
                            <Form.Item
                                label={t('AUTH.LABEL_CONFIRM_PASSWORD' as any)}
                                name="confirmPassword"
                                dependencies={['password']}
                                rules={[
                                    { required: true, message: t('AUTH.ERROR_CONFIRM_REQUIRED' as any) },
                                    ({ getFieldValue }) => ({
                                        validator(_, value) {
                                            if (!value || getFieldValue('password') === value) {
                                                return Promise.resolve();
                                            }
                                            return Promise.reject(new Error(t('AUTH.ERROR_PASSWORDS_MATCH' as any)));
                                        },
                                    }),
                                ]}
                            >
                                <Input.Password placeholder={t('AUTH.PLACEHOLDER_CONFIRM_PASSWORD' as any)} />
                            </Form.Item>
                        )}

                        {!isRegistering && (
                            <div className={styles.loginOptions}>
                                <Button type="link" disabled className={styles.linkButton}>
                                    {t('AUTH.FORGOT_PASSWORD' as any)}
                                </Button>
                                <Checkbox defaultChecked>
                                    {t('AUTH.REMEMBER_ME' as any)}
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
                            {isRegistering ? t('AUTH.BTN_REGISTER' as any) : t('AUTH.BTN_LOGIN' as any)}
                        </Button>

                        <div className={styles.loginRegister}>
                            {isRegistering ? t('AUTH.TEXT_HAS_ACCOUNT' as any) : t('AUTH.TEXT_NO_ACCOUNT' as any)}
                            <Button
                                type="link"
                                onClick={() => {
                                    setIsRegistering(!isRegistering);
                                    form.resetFields();
                                }}
                                className={styles.inlineLink}
                            >
                                {isRegistering ? t('AUTH.BTN_LOGIN' as any) : t('AUTH.BTN_REGISTER' as any)}
                            </Button>
                        </div>

                        {!isRegistering && (
                            <>
                                <div className={styles.loginDivider}>
                                    <span className={styles.dividerLine} />
                                    <span className={styles.dividerText}>{t('AUTH.DIVIDER_OR' as any)}</span>
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
                                                message.error(t('AUTH.ERROR_GOOGLE_FAIL' as any));
                                            } finally {
                                                setIsLoading(false);
                                            }
                                        }}
                                        onError={() => {
                                            message.error(t('AUTH.ERROR_GOOGLE_CANCEL' as any));
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