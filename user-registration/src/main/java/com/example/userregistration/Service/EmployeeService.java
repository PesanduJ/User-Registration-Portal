package com.example.userregistration.Service;

import com.example.userregistration.Entity.Employee;

public interface EmployeeService {

    Employee createEmployee(Employee employee);

    Employee findEmployeeByEmail(String email);
}
