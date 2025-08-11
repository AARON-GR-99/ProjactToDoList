import { useSelector, useDispatch } from 'react-redux';
import { login, logout, resetAuthState } from '../redux/slices/authSlice';
import authService from '../api/services/authService';
import { useTranslation } from 'react-i18next';

const useAuth = () => {
    const dispatch = useDispatch();
    const { t } = useTranslation('authorization');
    const { user, status, error } = useSelector((state) => state.auth);

    const loginUser = async (loginDto) => {
        try {
            const response = await dispatch(login(loginDto)).unwrap();
            return response;
        } catch (error) {
            const errorMessage = t('loginError') || 'An error occurred while trying to log in.';
            throw new Error(errorMessage);
        }
    };

    const logoutUser = async () => {
        try {
            await dispatch(logout()).unwrap();
        } catch (error) {
            console.error("Error during logout:", error);
        } finally {
            dispatch(resetAuthState());
        }
    };

    const forgotPassword = async (email) => {
        try {
            const response = await authService.sendResetEmail(email);
            return response;
        } catch (error) {
            const errorMessage = t('recoverPasswordError') || 'An error occurred while trying to send reset email.';
            throw new Error(errorMessage);
        }
    };

    const resetPassword = async (resetPasswordDto) => {
        try {
            const response = await authService.resetPassword(resetPasswordDto);
            return response;
        } catch (error) {
            const errorMessage = t('changePasswordError') || 'An error occurred while trying to reset the password.';
            throw new Error(errorMessage);
        }
    };

    return {
        user,
        status,
        error,
        loginUser,
        logoutUser,
        forgotPassword,
        resetPassword,
    };
};

export default useAuth;