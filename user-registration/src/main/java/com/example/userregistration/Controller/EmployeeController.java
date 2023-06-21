package com.example.userregistration.Controller;

import com.example.userregistration.Entity.Employee;
import com.example.userregistration.Service.EmailService;
import com.example.userregistration.Service.EmployeeService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;
import java.util.Random;

@RestController
@CrossOrigin
@RequestMapping("/api/v1")
public class EmployeeController {

    private final Map<String, Integer> otpMap = new HashMap<>();

    @Autowired
    private EmployeeService employeeService;

    @Autowired
    private EmailService emailService;

    @PostMapping("/create-employee")
    public Employee createNewEmployee(@RequestBody Employee employee){
        return employeeService.createEmployee(employee);
    }

    @GetMapping("/employee-login")
    public Boolean login(@RequestParam String email, @RequestParam String password) {
        Employee employee = employeeService.findEmployeeByEmail(email);

        if (employee != null && employee.getPassword().equals(password)) {
            return true;
        } else {
            return false;
        }
    }

    @PostMapping("/send-otp")
    public String sendOTP(@RequestParam String email){

        // Generate a random 4-digit number
        Random random = new Random();
        int otp = 1000+random.nextInt(9000);

        // Store the OTP in memory for verification
        otpMap.clear();
        otpMap.put(email, otp);

        String to = email;
        String subject = "Verification For Employment: " +otp;
        String body = "Your OTP is : "+otp;

        emailService.sendEmail(to, subject, body);

        return "Email sent successfully!";
    }

    @PostMapping("/verify-otp")
    public Boolean verifyOTP(@RequestParam String email, @RequestParam int otp) {
        Integer storedOTP = otpMap.get(email);

        if (storedOTP != null && storedOTP == otp) {
            // OTP is valid
            otpMap.remove(email); // Remove the OTP from memory after verification
            return true;
        } else {
            // OTP is invalid
            return false;
        }
    }
}
