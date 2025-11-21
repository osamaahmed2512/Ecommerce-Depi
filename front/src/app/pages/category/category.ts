import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NewsletterBanner } from '../../components/newsletter/newsletter';
import { ProductCard } from '../../components/top-selling/top-selling';

type FilterGroup = {
    title: string;
    options: { label: string; value: string }[];
};

@Component({
    selector: 'app-category-page',
    standalone: true,
    imports: [CommonModule, NewsletterBanner],
    templateUrl: './category.html',
    styleUrl: './category.css',
})
export class CategoryPage {
    protected readonly filters: FilterGroup[] = [
        {
            title: 'Category',
            options: [
                { label: 'T-Shirts', value: 'tshirt' },
                { label: 'Jeans', value: 'jeans' },
                { label: 'Coats', value: 'coats' },
                { label: 'Hoodies', value: 'hoodies' },
            ],
        },
        {
            title: 'Size',
            options: [
                { label: 'XS', value: 'xs' },
                { label: 'S', value: 's' },
                { label: 'M', value: 'm' },
                { label: 'L', value: 'l' },
                { label: 'XL', value: 'xl' },
            ],
        },
    ];

    protected readonly colors = ['#111827', '#f4a8a8', '#f97316', '#16a34a', '#a855f7', '#0ea5e9', '#facc15'];

    protected readonly products: ProductCard[] = [
        {
            name: 'Pastel Graphic Tee',
            price: 80,
            image: 'https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=600&q=80',
            rating: 4.4,
            reviews: 93,
        },
        {
            name: 'Casual Straight Jeans',
            price: 130,
            image: 'https://images.unsplash.com/photo-1503341455253-b2e723bb3dbb?auto=format&fit=crop&w=600&q=80',
            rating: 4.2,
            reviews: 120,
        },
        {
            name: 'Bold Plaid Shirt',
            price: 115,
            image: 'https://images.unsplash.com/photo-1514996937319-344454492b37?auto=format&fit=crop&w=600&q=80',
            rating: 4.7,
            reviews: 174,
        },
        {
            name: 'Sunset Statement Tee',
            price: 95,
            image: 'https://images.unsplash.com/photo-1489987707025-afc232f7ea0f?auto=format&fit=crop&w=600&q=80',
            rating: 4.8,
            reviews: 201,
        },
        {
            name: 'Relaxed Knit',
            price: 150,
            image: 'https://images.unsplash.com/photo-1475180098004-ca77a66827be?auto=format&fit=crop&w=600&q=80',
            rating: 4.5,
            reviews: 166,
        },
        {
            name: 'Utility Cargo Shorts',
            price: 110,
            image: 'https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?auto=format&fit=crop&w=600&q=80',
            rating: 4.1,
            reviews: 88,
        },
        {
            name: 'Weekend Jersey',
            price: 70,
            image: 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=600&q=80',
            rating: 4,
            reviews: 63,
        },
        {
            name: 'Studio Denim',
            price: 145,
            image: 'https://images.unsplash.com/photo-1473966968600-fa801b869a1a?auto=format&fit=crop&w=600&q=80',
            rating: 4.5,
            reviews: 137,
        },
    ];

}

