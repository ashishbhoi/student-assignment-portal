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
            fontFamily: {
                sans: ['Inter', 'sans-serif'], // Ensure a clean font stack
            },
            colors: {
                primary: "#18181b", // Zinc 900 (Near Black) for high contrast primary actions in light mode
                "primary-hover": "#27272a", // Zinc 800
                "primary-dark": "#fafafa", // Zinc 50 (Near White) for dark mode primary
                
                accent: "#6366f1", // Indigo 500 - The subtle accent
                "accent-hover": "#4f46e5", // Indigo 600

                background: "#ffffff",
                surface: "#fafafa", // Zinc 50
                border: "#e4e4e7", // Zinc 200

                error: "#ef4444", // Red 500
                
                "on-primary": "#ffffff", // Text on primary (Light mode)
                "on-background": "#09090b", // Zinc 950
                "on-surface": "#18181b", // Zinc 900
                "on-surface-muted": "#71717a", // Zinc 500

                // Dark mode specific overrides
                "dark-background": "#09090b", // Zinc 950
                "dark-surface": "#18181b", // Zinc 900
                "dark-border": "#27272a", // Zinc 800
                "dark-on-primary": "#09090b", // Text on primary (Dark mode - button is white)
                "dark-on-background": "#fafafa", // Zinc 50
                "dark-on-surface": "#f4f4f5", // Zinc 100
                "dark-on-surface-muted": "#a1a1aa", // Zinc 400
            },
        },
    },
    plugins: [],
};
