import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

export type ProductCard = {
  name: string;
  price: number;
  originalPrice?: number;
  rating: number;
  reviews: number;
  image: string;
  badge?: string;
  colors?: string[];
};

@Component({
  selector: 'app-product-shelf',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './top-selling.html',
  styleUrl: './top-selling.css',
})
export class ProductShelfComponent {
  @Input({ required: true }) title!: string;
  @Input() description = '';
  @Input() actionLabel = 'Shop Now';
  @Input() products: ProductCard[] = [];

  protected readonly trackByProduct = (_: number, product: ProductCard) => product.name;

  protected buildStars(rating: number) {
    return Array.from({ length: 5 }).map((_, index) => index < Math.round(rating));
  }
}
