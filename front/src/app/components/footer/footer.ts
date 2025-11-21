import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

type FooterColumn = {
  title: string;
  links: { label: string; href: string }[];
};

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './footer.html',
  styleUrl: './footer.css',
})
export class Footer {
  protected readonly columns: FooterColumn[] = [
    {
      title: 'Shop',
      links: [
        { label: 'New Arrivals', href: '#' },
        { label: 'Best Sellers', href: '#' },
        { label: 'Trending', href: '#' },
        { label: 'Collections', href: '#' },
      ],
    },
    {
      title: 'Company',
      links: [
        { label: 'About', href: '#' },
        { label: 'Careers', href: '#' },
        { label: 'Sustainability', href: '#' },
        { label: 'Press', href: '#' },
      ],
    },
    {
      title: 'Support',
      links: [
        { label: 'Contact', href: '#' },
        { label: 'FAQs', href: '#' },
        { label: 'Shipping & Returns', href: '#' },
        { label: 'Size Guide', href: '#' },
      ],
    },
    {
      title: 'Account',
      links: [
        { label: 'My Account', href: '#' },
        { label: 'Orders', href: '#' },
        { label: 'Wishlist', href: '#' },
        { label: 'Privacy', href: '#' },
      ],
    },
  ];

  protected readonly currentYear = new Date().getFullYear();
}
