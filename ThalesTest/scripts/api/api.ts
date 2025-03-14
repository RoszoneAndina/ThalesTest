import { Product } from "../model/product.js";

const API_URL = "/api/product";

export async function getProducts(): Promise<Product[]> {
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error("Error al obtener la lista de productos");
    return response.json();
}

export async function getProductById(id: number): Promise<Product | null> {
    const response = await fetch(`${API_URL}/${id}`);
    if (!response.ok) return null;
    return response.json();
}
