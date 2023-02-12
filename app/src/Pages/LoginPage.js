import React from 'react'

const LoginPage = ({ users, onSuccessfulLogin }) => {

    const onSubmit = (e) => {
        e.preventDefault();
        const form = e.target;
        const formData = new FormData(form);
        const formJson = Object.fromEntries(formData.entries());
        const { email, password } = formJson;
        const user = users.filter(user => user.email === email)[0];
        if (user) {
            const passwordMatch = user.password === password;
            if (passwordMatch) {
                onSuccessfulLogin(user)
            }
        }
    }

    return (
        <form onSubmit={onSubmit} className='mt-5 row d-flex flex-column align-items-center justify-content-center'>
            <div className="col-md-6">
                <label htmlFor='email' className="col-sm-2 col-form-label">Email</label>
                <div className="col-sm-10">
                    <input required type="email" className="form-control" name="email" />
                </div>
            </div>
            <div className="col-md-6">
                <label htmlFor='password' className="col-sm-2 col-form-label">Password</label>
                <div className="col-sm-10">
                    <input required type="password" className="form-control" name="password" />
                </div>
            </div>
            <div className="col-md-6 mt-3">
                <button type='submit' className="btn btn-dark">login</button>
            </div>
        </form>
    )
}

export default LoginPage