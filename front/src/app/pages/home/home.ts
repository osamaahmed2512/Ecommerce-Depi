import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { NewsletterBanner } from '../../components/newsletter/newsletter';
import { ProductCard, ProductShelfComponent } from '../../components/top-selling/top-selling';

type DressStyle = {
    name: string;
    image: string;
    accent: string;
};

type Testimonial = {
    author: string;
    role: string;
    rating: number;
    content: string;
};

@Component({
    selector: 'app-home-page',
    standalone: true,
    imports: [CommonModule, ProductShelfComponent, NewsletterBanner],
    templateUrl: './home.html',
    styleUrl: './home.css',
})
export class HomePage {
    protected readonly heroStats = [
        { value: '200+', label: 'International Brands' },
        { value: '2,000+', label: 'High Quality Products' },
        { value: '30,000+', label: 'Happy Customers' },
    ];

    protected readonly brandLogos = ['VERSACE', 'ZARA', 'GUCCI', 'PRADA', 'Calvin Klein'];

    protected readonly newArrivals: ProductCard[] = [
        {
            name: 'Essential Cotton T-Shirt',
            price: 98,
            image: 'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&q=80',
            rating: 4.5,
            reviews: 210,
            badge: 'New Arrival',
            colors: ['#000000', '#f97316', '#475569'],
        },
        {
            name: 'Vintage Straight Denim',
            price: 120,
            image: 'https://images.unsplash.com/photo-1503341455253-b2e723bb3dbb?auto=format&fit=crop&w=600&q=80',
            rating: 4.7,
            reviews: 189,
            badge: 'Limited',
        },
        {
            name: 'Modern Plaid Overshirt',
            price: 140,
            originalPrice: 180,
            image: 'https://images.unsplash.com/photo-1514996937319-344454492b37?auto=format&fit=crop&w=600&q=80',
            rating: 4.3,
            reviews: 163,
        },
        {
            name: 'Orange Statement Tee',
            price: 80,
            image: 'https://images.unsplash.com/photo-1489987707025-afc232f7ea0f?auto=format&fit=crop&w=600&q=80',
            rating: 4.9,
            reviews: 241,
            colors: ['#f97316', '#111827'],
        },
    ];

    protected readonly topSelling: ProductCard[] = [
        {
            name: 'Relaxed Linen Shirt',
            price: 210,
            image: 'https://images.unsplash.com/photo-1521572267360-ee0c2909d518?auto=format&fit=crop&w=600&q=80',
            rating: 4.8,
            reviews: 312,
        },
        {
            name: 'Weekend Graphic Hoodie',
            price: 150,
            originalPrice: 190,
            image: 'https://images.unsplash.com/photo-1469334031218-e382a71b716b?auto=format&fit=crop&w=600&q=80',
            rating: 4.6,
            reviews: 278,
            badge: '-20%',
        },
        {
            name: 'Tailored Chinos',
            price: 175,
            image: 'https://images.unsplash.com/photo-1490111718993-d98654ce6cf7?auto=format&fit=crop&w=600&q=80',
            rating: 4.4,
            reviews: 198,
        },
        {
            name: 'Minimal Wool Coat',
            price: 240,
            image: 'https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=600&q=80',
            rating: 4.9,
            reviews: 410,
            badge: 'Bestseller',
        },
    ];

    protected readonly dressStyles: DressStyle[] = [
        {
            name: 'Casual',
            image: 'https://images.unsplash.com/photo-1544441893-675973e31985?auto=format&fit=crop&w=500&q=80',
            accent: 'from-amber-100 via-white to-white',
        },
        {
            name: 'Formal',
            image: 'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=500&q=80',
            accent: 'from-slate-100 via-white to-white',
        },
        {
            name: 'Party',
            image: 'https://images.unsplash.com/photo-1469334031218-e382a71b716b?auto=format&fit=crop&w=500&q=80',
            accent: 'from-pink-50 via-white to-white',
        },
        {
            name: 'Gym',
            image: 'https://images.unsplash.com/photo-1523381210434-271e8be1f52b?auto=format&fit=crop&w=500&q=80',
            accent: 'from-lime-50 via-white to-white',
        },
    ];

    protected readonly testimonials: Testimonial[] = [
        {
            author: 'Sarah Anderson',
            role: 'Verified Buyer',
            rating: 5,
            content:
                'The quality exceeded my expectations. The fabric feels premium and the fit is perfect. Shipping was also super fast!',
        },
        {
            author: 'Lucas Martin',
            role: 'Verified Buyer',
            rating: 4.5,
            content:
                'Love the curated selection. It makes shopping so much easier. Customer support helped me pick the right size.',
        },
        {
            author: 'Emily Jacobs',
            role: 'Verified Buyer',
            rating: 5,
            content:
                'Stylish pieces that I cannot find elsewhere. Returns were hassle-free when I needed a different color.',
        },
    ];

    protected readonly selectedStyle = signal('Casual');
}

