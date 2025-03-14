var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { getProducts, getProductById } from "../api/api.js";
export class ProductPage {
    constructor() {
        var _a, _b;
        this.inputElement = document.getElementById("productId");
        this.searchButton = document.getElementById("searchButton");
        this.resultElement = (_a = document.getElementById("result")) !== null && _a !== void 0 ? _a : document.createElement("div");
        this.productTableBodyElement = (_b = document.getElementById("productTableBody")) !== null && _b !== void 0 ? _b : document.createElement("tbody");
        this.searchButton.addEventListener("click", () => this.handleSearch());
        this.inputElement.addEventListener("keypress", (event) => {
            if (event.key === "Enter") {
                this.handleSearch();
            }
        });
        this.loadAllProducts();
    }
    handleSearch() {
        return __awaiter(this, void 0, void 0, function* () {
            const idText = this.inputElement.value.trim();
            if (!idText) {
                yield this.loadAllProducts();
                return;
            }
            const id = parseInt(idText, 10);
            if (isNaN(id)) {
                this.showMessage(this.resultElement, "Ingrese un ID valido");
                return;
            }
            try {
                const product = yield getProductById(id);
                if (product) {
                    this.updateTable([product]);
                }
                else {
                    this.showMessage(this.resultElement, "Producto no encontrado");
                }
            }
            catch (_a) {
                this.showMessage(this.resultElement, "Error al buscar el producto");
            }
        });
    }
    loadAllProducts() {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const products = yield getProducts();
                this.updateTable(products);
            }
            catch (_a) {
                this.showMessage(this.productTableBodyElement, "Error al cargar los productos");
            }
        });
    }
    updateTable(products) {
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
    showMessage(element, message) {
        const safeMessage = this.escapeHTML(message);
        element.innerHTML = `<div class="alert alert-warning">${safeMessage}</div>`;
    }
    escapeHTML(text) {
        const div = document.createElement("div");
        div.textContent = text;
        return div.innerHTML;
    }
}
//# sourceMappingURL=productPage.js.map