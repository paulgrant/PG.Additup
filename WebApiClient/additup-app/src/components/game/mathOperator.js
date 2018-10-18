import React, { PureComponent } from 'react'
import PropTypes from 'prop-types';
//import { OperatorModel } from './lib/';

class MathOperator extends PureComponent {
    // constructor(props: OperatorModel){
    // }
    render() {
        return (
            <div className="operator">
                {this.props.operator === 0 ? '+' : ''}
                {this.props.operator === 1 ? '-' : ''}
                {this.props.operator === 2 ? '/' : ''}
                {this.props.operator === 3 ? '*' : ''}
            </div>
        )
    }
}

MathOperator.propTypes = {
    operator: PropTypes.number
  }

export default MathOperator;