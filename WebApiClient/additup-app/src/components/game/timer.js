import React from 'react';

class Timer extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            reset: false,
            start: false,
            time: this.props.time,
            isOn: false
        }
        this.startTimer = this.startTimer.bind(this);
        this.stopTimer = this.stopTimer.bind(this);
        this.resetTimer = this.resetTimer.bind(this);
    }

    componentDidMount() {
        //this.startTimer();
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.time !== undefined && nextProps.time !== this.state.time ) {
            this.setState({ time: nextProps.time });
        }

        if (nextProps.timerOn === true && !this.state.isOn) {
            this.startTimer();
        }
        if (nextProps.timerOn === false && this.state.isOn) {
            this.stopTimer();
            this.resetTimer();
        }
    }

    startTimer() {
        this.setState({isOn: true});
        this.timer = setInterval(() => {
            var time = this.state.time;
            this.setState({ time: time - 1 });
            if (this.state.time < 1) {
                this.stopTimer();
            }
        }
            , 1000);
    }
    stopTimer() {
        this.setState({ isOn: false });
        clearInterval(this.timer);
        if (typeof this.props.timerFinished === 'function') {
            this.props.timerFinished();
        }
    }
    resetTimer() {
        this.stopTimer();
        this.setState({ time: 0 })
    }
    render() {
        return (
            <div className="timerPanel">
                <strong>Time Remaining</strong><h3>{this.state.time}</h3>
            </div>
        )
    }
}
export default Timer;