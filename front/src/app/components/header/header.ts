import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

type NavLink = {
  label: string;
  path: string;
};

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  protected readonly navLinks: NavLink[] = [
    { label: 'Home', path: '/' },
    { label: 'Category', path: '/category' },
    { label: 'Product', path: '/product/1' },
    { label: 'Cart', path: '/cart' },
  ];

  protected mobileMenuOpen = false;

  protected toggleMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
  }
}
