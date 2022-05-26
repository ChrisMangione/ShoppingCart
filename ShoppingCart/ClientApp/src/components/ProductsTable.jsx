import React, { useContext } from "react";

const ProductsTable = (props) => {
    const action = props.action;
    const actionName = props.actionName;
    const products = props.products;

    return ( 
        <div>
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Price</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {products.map((product, index) =>
                    <tr key={product.productid}>
                    <td>{product.name}</td>
                    <td>{product.description}</td>
                    <td>{product.price}</td>
                        <td><button onClick={() => action(product, index)}>{actionName}</button></td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>
        );
};

export default ProductsTable;