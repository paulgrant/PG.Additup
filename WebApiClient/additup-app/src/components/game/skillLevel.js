import React, { PureComponent } from 'react'

class SkillLevel extends PureComponent {
  render() {
    return (
      <div className="level">
        {this.props.level === 1 ? 'Beginner' : '' }
        {this.props.level === 2 ? 'Talented' : '' }
        {this.props.level === 3 ? 'Intermediate' : '' }
        {this.props.level === 4 ? 'Advanced' : '' }
        {this.props.level === 5 ? 'Expert' : '' }
      </div>
    )
  }
}

export default SkillLevel;
//Example of ranks: "Beginner", "Talented", "Intermediate", "Advanced", "Expert"