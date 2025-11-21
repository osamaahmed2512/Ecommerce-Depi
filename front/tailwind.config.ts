import type { Config } from 'tailwindcss';

export default {
    content: ['./src/**/*.{html,ts}'],
    theme: {
        extend: {
            colors: {
                'brand-dark': '#1a1a1a',
                'brand-muted': '#4c4e57',
                'brand-accent': '#f97316',
                'brand-stroke': '#e5e7eb',
                'brand-soft': '#f2f0f1',
            },
            fontFamily: {
                display: ['"Integral CF"', 'Sora', 'system-ui', 'sans-serif'],
                sans: ['Sora', 'Inter', 'system-ui', 'sans-serif'],
            },
            boxShadow: {
                card: '0 25px 60px -25px rgba(15, 23, 42, 0.25)',
            },
            borderRadius: {
                '2.5xl': '1.375rem',
            },
        },
    },
} satisfies Config;

