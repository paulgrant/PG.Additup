import React from 'react';
import Timer from './timer';
import SkillLevel from './skillLevel';
import Exercise from './exercise';
import * as Constants from '../../utils/constants';
import './games.css';

class Game extends React.Component {
    constructor(props){
        super(props)
        this.state = {
            userId: '',
            time: Constants.TIME_LIMIT,
            correctExercises: 0,
            skillLevel: 1,
            timerOn: false,
            resetExercise: false,
            startButtonVisible:true,
            showExercises: false
        }
        this.startExercises = this.startExercises.bind(this);
        this.timer = React.createRef();
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
        //self.setState({ timerOn: false });
        self.setState({ time: Constants.TIME_LIMIT });
    }

    timerFinished = () => {
        var self = this;
        self.setState({ startButtonVisible: true});
        self.setState({ showExercises: false });
    }

    startExercises = () => {
        var self = this;
        self.setState({ startButtonVisible: false })
        self.setState({ showExercises: true });
        self.setState({ resetExercise: true });
        self.setState({ timerOn: true });
    }

    setUserId = (userId) => {
        var self = this;
        self.setState({ userId: userId});
    }

    componentDidMount() {
        
    }

    render() {
        return (
            <div>
                <h1>Add it up!</h1>
                { (this.state.showExercises) ? (
                <div>
                    <Exercise reset={this.state.resetExercise} onSuccess={this.correctAnswerHandler} setUserId={this.setUserId} />
                    <div className="timerSkillPanel">
                        <Timer time={this.state.time} timerOn={this.state.timerOn} timerFinished={this.timerFinished} />
                        <SkillLevel level={this.state.skillLevel} />
                    </div>
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