/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: "class",
    content: [
        "./Areas/**/*.cshtml",
        "./Pages/**/*.cshtml",
        "./Views/**/*.cshtml",
        "./Shared/**/*.cshtml",
    ],
    theme: {
        extend: {
            colors: {
                primary: "#6200EE",
                "primary-dark": "#4a00d1",
                "primary-light": "#a055ff",
                "primary-variant": "#3700B3",
                secondary: "#03DAC6",
                "secondary-variant": "#018786",
                background: "#FFFFFF",
                surface: "#FFFFFF",
                error: "#B00020",
                "on-primary": "#FFFFFF",
                "on-secondary": "#000000",
                "on-background": "#000000",
                "on-surface": "#000000",
                "on-error": "#FFFFFF",

                // Dark mode colors
                "dark-background": "#121212",
                "dark-surface": "#1E1E1E",
                "dark-on-primary": "#FFFFFF",
                "dark-on-secondary": "#000000",
                "dark-on-background": "#FFFFFF",
                "dark-on-surface": "#FFFFFF",
                "dark-on-error": "#FFFFFF",
            },
        },
    },
    plugins: [],
};
