import React, { useState } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";

export const NavMenu = ({ setToken, token }) => {
  const [collapsed, setCollapsed] = useState();

  return (
    <header>
      <Navbar className="nav">
        <NavbarBrand tag={Link} to="/" className="menuLogo">
          One
        </NavbarBrand>
        <ul className="menuItems">
          {token == "" || token == undefined ? (
            <>
              <li>
                <NavLink tag={Link} to="/register">
                  Register
                </NavLink>
              </li>
              <li>
                <NavLink tag={Link} to="/login">
                  LogIn
                </NavLink>
              </li>
            </>
          ) : (
            <NavItem>
              <button onClick={setToken("")}>Log Out</button>
            </NavItem>
          )}
        </ul>
      </Navbar>
    </header >
  );
};
