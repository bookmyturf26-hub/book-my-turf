package com.bookmyturf.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "TURF")
public class Turf {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer turfID;

    @ManyToOne
    @JoinColumn(name = "TurfOwnerID")
    private User turfOwner;

    private String turfName;
    private String location;
    private String city;
    private String description;
    private String turfStatus;

    public Turf() {}

    public Turf(Integer turfID, User turfOwner, String turfName,
                String location, String city, String description, String turfStatus) {
        this.turfID = turfID;
        this.turfOwner = turfOwner;
        this.turfName = turfName;
        this.location = location;
        this.city = city;
        this.description = description;
        this.turfStatus = turfStatus;
    }

    public Integer getTurfID() { return turfID; }
    public void setTurfID(Integer turfID) { this.turfID = turfID; }

    public User getTurfOwner() { return turfOwner; }
    public void setTurfOwner(User turfOwner) { this.turfOwner = turfOwner; }

    public String getTurfName() { return turfName; }
    public void setTurfName(String turfName) { this.turfName = turfName; }

    public String getLocation() { return location; }
    public void setLocation(String location) { this.location = location; }

    public String getCity() { return city; }
    public void setCity(String city) { this.city = city; }

    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }

    public String getTurfStatus() { return turfStatus; }
    public void setTurfStatus(String turfStatus) { this.turfStatus = turfStatus; }
}
