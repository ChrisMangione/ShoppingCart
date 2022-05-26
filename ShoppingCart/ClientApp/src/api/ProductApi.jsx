
export const FetchProductList = async (curr) => {
    const response = await fetch('api/Product/GetProducts?currency=' + curr);
    const data = await response.json();
    return data;
  }

