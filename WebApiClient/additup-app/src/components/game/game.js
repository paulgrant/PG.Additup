import React from 'react';
import Timer from './timer';
import SkillLevel from './skillLevel';
import Exercise from './exercise';
import * as Constants from '../../utils/constants';

class Game extends React.Component {
    constructor(props){
        super(props)
        this.state = {
            correctExercises: 0,
            skillLevel: 1,
            timerStart: false,
            timerReset: false,
            resetExercise: false,
            startButtonVisible:true,
            showExercises: false
        }
        this.startExercises = this.startExercises.bind(this);
      }
    

    correctAnswerHandler = (value) => {
        var self = this;
        if(value === -1)
        {
            self.setState({correctExercises: 0});
        }
        else
        {
            self.setState({correctExercises: self.state.correctExercises + 1});
            var newSkillLevel = Math.floor(self.state.correctExercises / Constants.LEVEL_SIZE) + 1;
            self.setState({ skillLevel : newSkillLevel });
        }
        self.setState({ timerReset:!self.state.timerReset});
        self.setState({ resetExercise: !self.state.resetExercise });
    }

    timerFinished = () => {
        var self = this;
        self.setState({ startButtonVisible: true});
        self.setState({ showExercises: false })
    }

    startExercises = () => {
        var self = this;
        self.setState({ startButtonVisible: false })
        self.setState({ resetExercise: !self.state.resetExercise });
        self.setState({ timerStart: !self.state.timerStart });
        self.setState({ showExercises: true });
    }

    componentDidMount() {
        
    }

    render() {
        return (
            <div>
                <h1>Add it up!</h1>
                { (this.state.showExercises) ? (
                <div>
                    <Exercise reset={this.state.resetExercise} onSuccess={this.correctAnswerHandler} />
                    <Timer time={30} reset={this.state.timerReset} start={this.state.timerStart} timerFinished={this.timerFinished} />
                    <SkillLevel level={this.state.skillLevel} />
                </div>
                ) : null}
                { (this.state.startButtonVisible) ? (
                <button onClick={this.startExercises}>Start</button>
                ) : null}
            </div>
        );
    }
}

export default Game;