import React, { useContext } from "react";
import { CurrencyContext } from "./CurrencyProvider";
import { ProductContext } from "./ProductProvider";
import ProductsTable from "./ProductsTable";

const ProductList = () => {
    const { currentCurr } = useContext(CurrencyContext);
    const [ currency ] = currentCurr;
    const { productVal, cartVal } = useContext(ProductContext);
    const [ productList ] = productVal;
    const [ cartList, setcart ] = cartVal;

    const addProductToCart = (product, index) => {
        if (cartList != null)
            setcart(items => [...items, { productId: product.productId }]);
        else
            setcart([{ productId: product.productId }])
    };

    if (currency == null || productList == null) return null;
    return ( 
        <div>
            <b>Items in your cart:</b> {(cartList ? cartList.length : 0) }
            <ProductsTable action={addProductToCart} actionName="Add To Cart" products={productList}></ProductsTable>
        </div>
        );
};

export default ProductList;