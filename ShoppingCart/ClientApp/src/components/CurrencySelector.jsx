import React, { useContext  } from "react";
import Select, { components } from "react-select";
import { CurrencyContext } from "./CurrencyProvider";


const CurrencySelector = () => {
    const { currencyVal, currentCurr } = useContext(CurrencyContext);
    const [ currencyList ] = currencyVal;
    const [ currency, setCurrency] = currentCurr;

    const updateCurrentCurrency = e => {
        setCurrency(e);
    };

    return (
        <div>
            <Select
                options={currencyList}
                value={currency}
                onChange={updateCurrentCurrency}
                />
        </div>
        );
}

export default CurrencySelector;