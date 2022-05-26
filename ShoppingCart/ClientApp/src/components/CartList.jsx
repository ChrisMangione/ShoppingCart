import React, { useContext, useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { CurrencyContext } from "./CurrencyProvider";
import { ProductContext } from "./ProductProvider";
import ProductsTable from "./ProductsTable";
import { FetchShipping, PostCheckout } from "../api/CheckoutApi";

const CartList = () => {
    const { currentCurr } = useContext(CurrencyContext);
    const [ currency ] = currentCurr;
    const { productVal, cartVal } = useContext(ProductContext);
    const [ productList ] = productVal;
    const [ cartList, setcart ] = cartVal;
    const [shipping, setShipping] = useState('?',);
    const history = useHistory();

    const removeProductFromCart = (product, index) => {
        const obj = cartList;
        obj.splice(index, 1);
        setcart([...obj]);
        setShipping('?');
    };

    useEffect(() => {
        setShipping('?');
    }, [currency]);

    const getCart = () => {
        var cart = [];
        for (let index = 0; index < cartList.length; index++) {
            cart.push(productList.find(o => o.productId == cartList[index].productId))
        }
        return cart;
    };

    const calculateTotal = () => {
        return getCart().map(o => Number(o.price)).reduce((prev, next) => {
            return prev+next
        },0)};

    const getSetShipping =  async () => { 
        var shippingCost = await FetchShipping(calculateTotal(), currency.value);
        setShipping(shippingCost);
    }

    const checkout =  async () => { 
        var result = await PostCheckout(cartList, currency.value);
        setcart(null);
        history.push("/thankyou", [{ value: result, currency: currency }])
    }

    if (currency == null || cartList == null) return (
        <div><b>Items in your cart:</b> 0</div>
    );
    return ( 
        <div>
            <b>Items in your cart:</b> {(cartList ? cartList.length : 0) }
            <ProductsTable action={removeProductFromCart} actionName="Remove From Cart" products={getCart()}></ProductsTable>
            <b>Total Cost:</b> {calculateTotal().toFixed(2)} {currency.value}
            <div/>
            <button onClick={() => getSetShipping()}>Calculate Shipping</button> 
            <div/>
            <b>Shipping Cost:</b> {shipping} {currency.value}
            <div/>
            <button onClick={() => checkout()} disabled={calculateTotal() === 0}>Purchase</button>
        </div>
        );
};

export default CartList;