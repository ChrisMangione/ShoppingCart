import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Thankyou from './components/Thankyou';
import ProductList from './components/ProductList';
import CartList from './components/CartList';
import { CurrencyProvider } from './components/CurrencyProvider';
import { ProductProvider } from './components/ProductProvider';
import './custom.css'


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <CurrencyProvider>
        <ProductProvider>
          <Layout>
              <Route exact path='/' component={ProductList} />
              <Route path='/cart' component={CartList} />
              <Route path='/thankyou' component={Thankyou} />
          </Layout>
        </ProductProvider>
      </CurrencyProvider>
    );
  }
}

