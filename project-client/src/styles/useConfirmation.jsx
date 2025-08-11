import Swal from 'sweetalert2';

// Helper global para usar sin hook
const showConfirmation = async ({ title, text, confirmText, cancelText, icon, theme = 'light' }) => {
    return await Swal.fire({
        title,
        text,
        icon,
        showCancelButton: true,
        confirmButtonColor: theme === 'dark' ? '#14b8a6' : '#0d9488',
        cancelButtonColor: '#ef4444',
        background: theme === 'dark' ? '#1f2937' : '#ffffff',
        color: theme === 'dark' ? '#f3f4f6' : '#1f2937',
        confirmButtonText: confirmText,
        cancelButtonText: cancelText,
    });
};

export default showConfirmation;