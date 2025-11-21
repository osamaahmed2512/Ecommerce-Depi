import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { NewsletterBanner } from '../../components/newsletter/newsletter';

type CartItem = {
    name: string;
    color: string;
    size: string;
    price: number;
    quantity: number;
    image: string;
};

@Component({
    selector: 'app-cart-page',
    standalone: true,
    imports: [CommonModule, NewsletterBanner],
    templateUrl: './cart.html',
    styleUrl: './cart.css',
})
export class CartPage {
    protected readonly cartItems = signal<CartItem[]>([
        {
            name: 'One Life Graphic Tee',
            color: 'Black',
            size: 'Large',
            price: 110,
            quantity: 1,
            image: 'https://images.unsplash.com/photo-1514996937319-344454492b37?auto=format&fit=crop&w=600&q=80',
        },
        {
            name: 'Modern Straight Jeans',
            color: 'Denim',
            size: '32',
            price: 145,
            quantity: 1,
            image: 'https://images.unsplash.com/photo-1503341455253-b2e723bb3dbb?auto=format&fit=crop&w=600&q=80',
        },
    ]);

    protected readonly shipping = signal(12);
    protected readonly discount = signal(20);

    protected readonly subtotal = computed(() =>
        this.cartItems().reduce((sum, item) => sum + item.price * item.quantity, 0)
    );

    protected readonly total = computed(() => this.subtotal() + this.shipping() - this.discount());

    protected updateQuantity(index: number, delta: number) {
        this.cartItems.update((items) =>
            items.map((item, idx) =>
                idx === index ? { ...item, quantity: Math.max(1, item.quantity + delta) } : item
            )
        );
    }

    protected removeItem(index: number) {
        this.cartItems.update((items) => items.filter((_, idx) => idx !== index));
    }
}

