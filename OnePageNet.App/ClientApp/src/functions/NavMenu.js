import React, { useState } from "react";
import { Navbar, NavbarBrand, NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import "./styles/NavMenu.css";
import { useHistory } from "react-router-dom";

export function NavMenu({ setToken, token }) {
  const history = useHistory();

  const handleClick = (e) => {
    e.preventDefault();
    setToken(null);
    sessionStorage.removeItem("token");
    history.push("/");
  };

  return (
    <header>
      <Navbar className="nav">
        <NavbarBrand tag={Link} to="/" className="menuLogo">
          OnePage
        </NavbarBrand>
        <ul className="menuItems">
          {token == null ? (
            <>
              <li>
                <NavItem>
                  <NavLink tag={Link} to="/register">
                    Register
                  </NavLink>
                </NavItem>
              </li>
              <li>
                <NavItem>
                  <NavLink tag={Link} to="/login">
                    LogIn
                  </NavLink>
                </NavItem>
              </li>
            </>
          ) : (
            <>
              <li>
                <NavItem>
                  <NavLink tag={Link} to="/users">
                    Users
                  </NavLink>
                </NavItem>
              </li>
              <li>
                <NavItem>
                  <button onClick={(e) => handleClick(e)}>Log Out</button>
                </NavItem>
              </li>
            </>
          )}
        </ul>
      </Navbar>
    </header>
  );
}
