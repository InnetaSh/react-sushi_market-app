import { ROLES } from '@constants/roles';
import { ROUTES } from '@constants/routes';

export const getRedirectPath = (roles: string[] = []): string => {
    if (roles.includes(ROLES.MAIN_ADMIN)) {
        return ROUTES.ADMIN;
    }
    return ROUTES.HOME;
};