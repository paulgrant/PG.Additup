import React, { PureComponent } from 'react'

class MathOperator extends PureComponent {
  render() {
    return (
      <div className="operator">
        {this.props.operator === 0 ? '+' : '' }
        {this.props.operator === 1 ? '-' : '' }
        {this.props.operator === 2 ? '/' : '' }
        {this.props.operator === 3 ? '*' : '' }
      </div>
    )
  }
}

export default MathOperator;