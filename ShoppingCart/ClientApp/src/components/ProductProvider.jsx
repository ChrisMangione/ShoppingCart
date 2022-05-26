import React, {useState, createContext, useEffect, useContext} from "react";
import { CurrencyContext } from "./CurrencyProvider";
import { FetchProductList } from "../api/ProductApi";

export const ProductContext = createContext();

export const ProductProvider = props => {
    const { currentCurr } = useContext(CurrencyContext);
    const [ currency ] = currentCurr;

    const [ productList, setProductList ] = useState(null);
    const [ cartList, setCart ] = useState(null);

    useEffect(() => {
        currency && getSetProducts(currency.value);
    }, [currency]);

    const getSetProducts = async (curr) => { 
        var data = await FetchProductList(curr)
        setProductList(data);
    }

    return (
        <ProductContext.Provider value={{productVal: [productList, setProductList], cartVal: [cartList, setCart] }}>
            {props.children}
        </ProductContext.Provider>
        );
};
