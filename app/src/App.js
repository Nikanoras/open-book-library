import React, { useState, useEffect } from "react";
import { Routes, Route, useNavigate } from "react-router-dom"
import LoginPage from "./Pages/LoginPage";
import MainPage from "./Pages/MainPage";
import users from "./dummy/users.json"

const App = () => {
  const [user, setUser] = useState(null)
  const nav = useNavigate()

  useEffect(() => {
    nav("/login")
  }, [user])

  const onSuccessfulLogin = (newUser) => {
    setUser(newUser)
    nav("/")
  }


  return <div className="App">
    <Routes>
      <Route path="/" element={<MainPage />}></Route>
      <Route path="/login" element={<LoginPage onSuccessfulLogin={onSuccessfulLogin} users={users} />}></Route>
    </Routes>
  </div>;
}

export default App;
