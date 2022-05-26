import React, {useState, createContext, useEffect} from "react";
import { FetchCurrencyList } from "../api/CurrencyApi";

export const CurrencyContext = createContext();

export const CurrencyProvider = props => {
    const [ currencyList, setCurrencyList ] = useState(null);
    const [ currency, setCurrency ] = useState(null);

    useEffect(() => {
        getSetCurrencyList();
    }, []);

    const getSetCurrencyList = async () => { 
        var data = await FetchCurrencyList()
        setCurrencyList(data);
        setCurrency(data[0]);
    }

    return (
        <CurrencyContext.Provider value={{currencyVal: [currencyList, setCurrencyList], currentCurr: [ currency, setCurrency] }}>
            {props.children}
        </CurrencyContext.Provider>
        );
};
