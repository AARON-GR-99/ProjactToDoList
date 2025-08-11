const getCustomSelectStyles = (theme) => {
    const isDark = theme === 'dark';

    return {
        control: (provided, state) => ({
            ...provided,
            backgroundColor: state.selectProps.menuIsOpen
                ? (isDark ? '#1f2937' : '#f3f4f6') // gray-800 : gray-100
                : (isDark ? '#374151' : '#ffffff'), // gray-700 : white
            borderColor: state.isFocused ? '#14b8a6' : (isDark ? '#4b5563' : '#d1d5db'), // gray-600 : gray-300
            boxShadow: state.isFocused ? '0 0 0 1px #14b8a6' : 'none',
            color: isDark ? '#f9fafb' : '#1f2937',
            '&:hover': {
                borderColor: '#14b8a6'
            }
        }),
        menu: (provided) => ({
            ...provided,
            backgroundColor: isDark ? '#1f2937' : '#ffffff',
            color: isDark ? '#f9fafb' : '#1f2937',
        }),
        option: (provided, state) => ({
            ...provided,
            backgroundColor: state.isFocused
                ? '#14b8a6'
                : (isDark ? '#1f2937' : '#ffffff'),
            color: state.isFocused
                ? '#ffffff'
                : (isDark ? '#f9fafb' : '#1f2937'),
            '&:active': {
                backgroundColor: '#0d9488'
            }
        }),
        singleValue: (provided) => ({
            ...provided,
            color: isDark ? '#f9fafb' : '#1f2937'
        }),
        multiValue: (provided) => ({
            ...provided,
            backgroundColor: '#14b8a6',
            color: '#ffffff'
        }),
        multiValueLabel: (provided) => ({
            ...provided,
            color: '#ffffff'
        }),
        multiValueRemove: (provided) => ({
            ...provided,
            color: '#ffffff',
            ':hover': {
                backgroundColor: '#0d9488',
                color: '#ffffff'
            }
        })
    };
};

export default getCustomSelectStyles;