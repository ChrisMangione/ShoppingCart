export const FetchShipping = async (total, curr) => { 
    const response = await fetch('api/Checkout/CalculateShipping?totalCost=' + total + '&Currency=' + curr);
    var data = await response.json();
    return data;
}

export const PostCheckout = async (products, curr) => {
    let value = Object.entries(products).map(([key, value]) => (value.productId));
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ currency: curr, products: value })
    };
    const response = await fetch('api/Checkout/Checkout', requestOptions);
    var data = await response.json();
    return data;
}