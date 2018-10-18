import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import MathOperator from './components/game/mathOperator';
import SkillLevel from './components/game/skillLevel';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
  ReactDOM.unmountComponentAtNode(div);
});


it('renders correct Skill level', () => {
  const div = document.createElement('div');
  ReactDOM.render(<SkillLevel level={2} />, div);
  ReactDOM.unmountComponentAtNode(div);
});