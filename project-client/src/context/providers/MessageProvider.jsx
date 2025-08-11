import React from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import MessageContext from '../contexts/MessageContext';

const MessageProvider = ({ children }) => {
    const showMessage = (message, options = {}) => {
        toast(message, {
            ...options,
            className: 'bg-surface-light dark:bg-surface-dark text-text-primary-light dark:text-text-primary-dark',
            bodyClassName: 'text-sm',
            progressClassName: 'bg-primary-light dark:bg-primary-dark',
        });
    };

    return (
        <MessageContext.Provider value={{ showMessage }}>
            {children}
            <ToastContainer
                position="top-right"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                className="text-sm"
            />
        </MessageContext.Provider>
    );
};

export default MessageProvider;