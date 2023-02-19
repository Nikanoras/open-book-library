import React from 'react'

const Loading = () => {
    return (
        <div className="spinner-grow" style={{ width: "5rem", height: "5rem" }} role="status">
            <span className="visually-hidden">Loading...</span>
        </div>
    )
}

export default Loading