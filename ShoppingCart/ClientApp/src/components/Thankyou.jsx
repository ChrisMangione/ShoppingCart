import React from "react";

const Thankyou = ({ location }) => {
    const state = location.state;

    if (state === undefined) return <h1 class="text-center">No purchase made</h1>;
    return ( 
        <div class="text-center">
            <h1>Thank you!</h1>
            <h3>Total cost was: {state[0].value.toFixed(2)} {state[0].currency.value} </h3>
        </div>
        );
};

export default Thankyou;