package com.bookmyturf.entity;

import jakarta.persistence.*;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import com.bookmyturf.enums.TurfStatus;

import java.time.LocalDateTime;
import java.util.Set;

@Entity
@Table(name = "TURF")
public class Turf {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "TurfID")
    private Integer turfId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "TurfOwnerID", nullable = false)
    private User turfOwner;

    @Column(name = "TurfName", nullable = false)
    private String turfName;

    @Column(name = "Location", nullable = false)
    private String location;

    @Column(name = "City", nullable = false)
    private String city;

    @Column(name = "Description")
    private String description;

    @Enumerated(EnumType.STRING)
    @Column(name = "TurfStatus")
    private TurfStatus turfStatus;

    @CreationTimestamp
    @Column(name = "CreatedDate", updatable = false)
    private LocalDateTime createdDate;

    @UpdateTimestamp
    @Column(name = "UpdatedDate")
    private LocalDateTime updatedDate;

    @ManyToMany
    @JoinTable(
        name = "TURF_SPORTS",
        joinColumns = @JoinColumn(name = "TurfID"),
        inverseJoinColumns = @JoinColumn(name = "SportID")
    )
    private Set<Sports> sports;

    @OneToMany(mappedBy = "turf", cascade = CascadeType.ALL, orphanRemoval = true)
    private Set<TurfPhotos> photos;

    @ManyToMany
    @JoinTable(
        name = "TURF_AMENITIES",
        joinColumns = @JoinColumn(name = "TurfID"),
        inverseJoinColumns = @JoinColumn(name = "AmenityID")
    )
    private Set<Amenity> amenities;

	public Integer getTurfId() {
		return turfId;
	}

	public void setTurfId(Integer turfId) {
		this.turfId = turfId;
	}

	public User getTurfOwner() {
		return turfOwner;
	}

	public void setTurfOwner(User turfOwner) {
		this.turfOwner = turfOwner;
	}

	public String getTurfName() {
		return turfName;
	}

	public void setTurfName(String turfName) {
		this.turfName = turfName;
	}

	public String getLocation() {
		return location;
	}

	public void setLocation(String location) {
		this.location = location;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public TurfStatus getTurfStatus() {
		return turfStatus;
	}

	public void setTurfStatus(TurfStatus turfStatus) {
		this.turfStatus = turfStatus;
	}

	public LocalDateTime getCreatedDate() {
		return createdDate;
	}

	public void setCreatedDate(LocalDateTime createdDate) {
		this.createdDate = createdDate;
	}

	public LocalDateTime getUpdatedDate() {
		return updatedDate;
	}

	public void setUpdatedDate(LocalDateTime updatedDate) {
		this.updatedDate = updatedDate;
	}

	public Set<Sports> getSports() {
		return sports;
	}

	public void setSports(Set<Sports> sports) {
		this.sports = sports;
	}

	public Set<TurfPhotos> getPhotos() {
		return photos;
	}

	public void setPhotos(Set<TurfPhotos> photos) {
		this.photos = photos;
	}

	public Set<Amenity> getAmenities() {
		return amenities;
	}

	public void setAmenities(Set<Amenity> amenities) {
		this.amenities = amenities;
	}

}
