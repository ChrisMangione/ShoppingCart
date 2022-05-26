export const FetchCurrencyList = async () => { 
    const response = await fetch('api/Currency');
    var data = await response.json();
    return data.map((item) => ({label: item.country, value: item.currencyCode}));
}