package com.example.userregistration.Entity;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class Employee {

    @Id
    @Column(length = 100)
    private String employeeId;
    private String designation;
    private String email;
    private String phoneNo;
    private String password;
}
