import React, { Component } from 'react';
import logo from './logo-default.png';
import Game from './components/game/game';
import './App.css';

class App extends Component {
  render() {
    return (
      <div className="App">
        <img src={logo} alt="BNP Paribas" />
        <Game />
      </div>
    );
  }
}

export default App;
