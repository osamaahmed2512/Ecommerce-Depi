import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home';
import { ProductDetailPage } from './pages/product-detail/product-detail';
import { CategoryPage } from './pages/category/category';
import { CartPage } from './pages/cart/cart';

export const routes: Routes = [
    { path: '', component: HomePage },
    { path: 'product/:id', component: ProductDetailPage },
    { path: 'category', component: CategoryPage },
    { path: 'cart', component: CartPage },
    { path: '**', redirectTo: '' },
];
