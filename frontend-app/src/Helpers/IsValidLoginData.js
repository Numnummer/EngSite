export default function validateLoginData(loginData) {
  try {
    validateFullName(loginData.fullName);
    validateLogin(loginData.login);
    validateEmail(loginData.email);
    validatePasswordByLength(loginData.password);
    validatePasswordByUpperandLowerCase(loginData.password);
    validateConfirm(loginData.password, loginData.confirm);
    return "OK";
  } catch (error) {
    return error.message;
  }
}

export function validateSignInData(signInData) {
  try {
    validateLogin(signInData.login);
    validatePasswordByLength(signInData.password);
    validatePasswordByUpperandLowerCase(signInData.password);
    return "OK";
  } catch (error) {
    return error.message;
  }
}

function validateFullName(fullName) {
  if (fullName.length > 0) {
    return "OK";
  } else {
    throw new Error("Full name field must have at least 1 character");
  }
}

function validateEmail(email) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (emailRegex.test(email)) {
    return "OK";
  } else {
    throw new Error("Email not valid");
  }
}

function validateLogin(login) {
  if (login.length > 0) {
    return "OK";
  } else {
    throw new Error("login field must have at least 1 character");
  }
}

function validatePasswordByLength(password) {
  if (password.length >= 6) {
    return "OK";
  } else {
    throw new Error("password field must have at least 6 characters");
  }
}

function validatePasswordByUpperandLowerCase(password) {
  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  if (hasLowerCase && hasUpperCase) {
    return "OK";
  } else {
    throw new Error(
      "password field must have characters in upper and lower case"
    );
  }
}

function validateConfirm(password, confirm) {
  if (password === confirm) {
    return "OK";
  } else {
    throw new Error("Confirmed password and entered password are not same");
  }
}
