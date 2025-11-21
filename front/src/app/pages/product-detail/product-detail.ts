import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { NewsletterBanner } from '../../components/newsletter/newsletter';
import { ProductCard, ProductShelfComponent } from '../../components/top-selling/top-selling';

type Review = {
    author: string;
    rating: number;
    date: string;
    content: string;
};

@Component({
    selector: 'app-product-detail-page',
    standalone: true,
    imports: [CommonModule, ProductShelfComponent, NewsletterBanner],
    templateUrl: './product-detail.html',
    styleUrl: './product-detail.css',
})
export class ProductDetailPage {
    protected readonly product = {
        name: 'One Life Graphic T-Shirt',
        price: 250,
        originalPrice: 300,
        description:
            'Crafted from heavyweight organic cotton with a relaxed fit and dropped shoulders for an effortless drape. The signature graphic is screen printed for long lasting color.',
        features: ['100% organic cotton', 'Relaxed fit', 'Pre-washed to reduce shrinkage', 'Made in Portugal'],
        images: [
            'https://images.unsplash.com/photo-1514996937319-344454492b37?auto=format&fit=crop&w=800&q=80',
            'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=800&q=80',
            'https://images.unsplash.com/photo-1489987707025-afc232f7ea0f?auto=format&fit=crop&w=800&q=80',
        ],
    };

    protected readonly colors = ['#2f2f37', '#d8d8d8', '#8f8f8f', '#f97316'];
    protected readonly sizes = ['XS', 'S', 'M', 'L', 'XL'];
    protected readonly reviews: Review[] = [
        {
            author: 'Brooklyn Simmons',
            rating: 5,
            date: 'Sep 10, 2025',
            content:
                'The fabric feels premium and the fit is relaxed without being sloppy. The print held up great after multiple washes.',
        },
        {
            author: 'Robert Fox',
            rating: 4,
            date: 'Sep 02, 2025',
            content:
                'Color is rich and the cotton is soft. I sized up for a roomier fit. Shipping was quick and packaging was thoughtful.',
        },
        {
            author: 'Leslie Alexander',
            rating: 5,
            date: 'Aug 28, 2025',
            content:
                'Love the detail in the graphic. It elevates a simple outfit instantly. Will definitely purchase more from this drop.',
        },
    ];

    protected readonly suggestions: ProductCard[] = [
        {
            name: 'Pastel Comfort Tee',
            price: 120,
            image: 'https://images.unsplash.com/photo-1521572267360-ee0c2909d518?auto=format&fit=crop&w=600&q=80',
            rating: 4.2,
            reviews: 124,
        },
        {
            name: 'Gradient Artist Shirt',
            price: 130,
            image: 'https://images.unsplash.com/photo-1490111718993-d98654ce6cf7?auto=format&fit=crop&w=600&q=80',
            rating: 4.6,
            reviews: 201,
        },
        {
            name: 'Muted Earthy Tee',
            price: 90,
            originalPrice: 120,
            image: 'https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=600&q=80',
            rating: 4.4,
            reviews: 175,
        },
        {
            name: 'Contrast Sleeve Tee',
            price: 110,
            image: 'https://images.unsplash.com/photo-1521572267360-ee0c2909d518?auto=format&fit=crop&w=600&q=80',
            rating: 4.1,
            reviews: 132,
        },
    ];

    protected readonly selectedImage = signal(this.product.images[0]);
    protected readonly selectedSize = signal('M');
    protected readonly selectedColor = signal(this.colors[0]);
    protected quantity = signal(1);

    protected increment() {
        this.quantity.update((current) => Math.min(current + 1, 5));
    }

    protected decrement() {
        this.quantity.update((current) => Math.max(current - 1, 1));
    }
}

