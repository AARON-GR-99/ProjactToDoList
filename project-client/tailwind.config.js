/** @type {import('tailwindcss').Config} */
export default {
    darkMode: 'class',
    content: [
        "./index.html",
        "./src/**/*.{js,ts,jsx,tsx}",
    ],
    theme: {
        extend: {
            colors: {
                'arm-dark': '#004d55',
                'arm-light': '#60d7c5',
                'arm-gradient-start': '#36a699',
                'arm-gradient-end': '#002e40',

                'arm-text': '#022f36',
                'arm-header-text': '#ffffff',

                'arm-hover': '#57c4b1',
                'arm-active': '#003840',
                'arm-focus': '#36a699',
                'arm-disabled': '#b0e1db',
                
                primary: {
                    light: '#00CCCC',
                    DEFAULT: '#008080',
                    dark: '#004040',
                },
                secondary: {
                    light: '#00FFFF',
                    DEFAULT: '#00CCCC',
                    dark: '#044e4e',
                },
                background: {
                    light: '#ffffff',
                    DEFAULT: '#E5E5E5',
                    dark: '#0F0F0F',
                },
                surface: {
                    light: '#E0E0E0',
                    DEFAULT: '#D5D5D5',
                    dark: '#1F1F1F',
                },
                text: {
                    primary: {
                        light: '#333333',
                        DEFAULT: '#222222',
                        dark: '#E5E7EB',
                    },
                    secondary: {
                        light: '#555555',
                        DEFAULT: '#444444',
                        dark: '#9CA3AF',
                    },
                },
                error: {
                    light: '#F44336',
                    DEFAULT: '#E53935',
                    dark: '#EF4444',
                },
                warning: {
                    light: '#FF9800',
                    DEFAULT: '#FB8C00',
                    dark: '#F59E0B',
                },
                success: {
                    light: '#4CAF50',
                    DEFAULT: '#43A047',
                    dark: '#10B981',
                },
                info: {
                    light: '#2196F3',
                    DEFAULT: '#1E88E5',
                    dark: '#3B82F6',
                },
                neutral: {
                    light: '#B0B0B0',
                    DEFAULT: '#888888',
                    dark: '#333333',
                },
            },
            fontFamily: {
                sans: ['Poppins', 'Roboto', 'Arial', 'sans-serif'],
                pageSans:['Roboto', 'sans-serif'],
            },
        },
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/typography'),
        require('@tailwindcss/aspect-ratio'),
        require('tailwind-scrollbar')({ nocompatible: true }),
    ],
}