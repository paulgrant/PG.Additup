import React from 'react';

class Timer extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
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
        if (nextProps.time !== undefined) {
            this.stopTimer();
            this.setState({ time: nextProps.time });
        }

        if (nextProps.start !== undefined) {
            this.stopTimer();
            this.startTimer();
        }
    }

    startTimer() {
        this.setState({
            isOn: true
        })
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
        this.setState({ time: 0 })
    }
    render() {
        return (
            <div>
                <h3>timer: {this.state.time}</h3>
            </div>
        )
    }
}
export default Timer;