import { getProducts, getProductById } from "../api/api.js";

export class ProductPage {
    private inputElement: HTMLInputElement;
    private searchButton: HTMLButtonElement;
    private resultElement: HTMLElement;
    private productTableBodyElement: HTMLElement;

    constructor() {
        this.inputElement = document.getElementById("productId") as HTMLInputElement;
        this.searchButton = document.getElementById("searchButton") as HTMLButtonElement;
        this.resultElement = document.getElementById("result") ?? document.createElement("div");
        this.productTableBodyElement = document.getElementById("productTableBody") ?? document.createElement("tbody");

        this.searchButton.addEventListener("click", () => this.handleSearch());
        this.inputElement.addEventListener("keypress", (event) => {
            if (event.key === "Enter") {
                this.handleSearch();
            }
        });

        this.loadAllProducts();
    }

    private async handleSearch(): Promise<void> {
        const idText = this.inputElement.value.trim();

        if (!idText) {
            await this.loadAllProducts(); 
            return;
        }

        const id = parseInt(idText, 10);
        if (isNaN(id)) {
            this.showMessage(this.resultElement, "Ingrese un ID valido");
            return;
        }

        try {
            const product = await getProductById(id);
            if (product) {
                this.updateTable([product]); 
            } else {
                this.showMessage(this.resultElement, "Producto no encontrado");
            }
        } catch {
            this.showMessage(this.resultElement, "Error al buscar el producto");
        }
    }

    private async loadAllProducts(): Promise<void> {
        try {
            const products = await getProducts();
            this.updateTable(products);
        } catch {
            this.showMessage(this.productTableBodyElement, "Error al cargar los productos");
        }
    }

    private updateTable(products: any[]): void {
        console.log(products);
        if (products.length === 0) {
            this.productTableBodyElement.innerHTML = "<tr><td colspan='6'>No hay productos disponibles</td></tr>";
            return;
        }

        this.productTableBodyElement.innerHTML = products.map(({ id, images, title, price, tax, description }) => `
            <tr>
                <td>${id}</td>
                <td><img src="${images[0]}" alt="${title}" class="img-thumbnail" style="width:50px; height:auto;"></td>
                <td>${title}</td>
                <td>$${price.toFixed(2)}</td>
                <td>$${tax.toFixed(2)}</td>
                <td>${description}</td>
            </tr>
        `).join("");
    }

    private showMessage(element: HTMLElement, message: string): void {
        const safeMessage = this.escapeHTML(message);
        element.innerHTML = `<div class="alert alert-warning">${safeMessage}</div>`;
    }

    private escapeHTML(text: string): string {
        const div = document.createElement("div");
        div.textContent = text;
        return div.innerHTML;
    }
}
