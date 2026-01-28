
	package com.bookmyturf.entity;

	import jakarta.persistence.*;
	import java.util.Set;

	@Entity
	@Table(name = "AMENITIES")
	public class Amenity {
	    @Id
	    @GeneratedValue(strategy = GenerationType.IDENTITY)
	    @Column(name = "AmenityID")
	    private Integer amenityId;

	    @Column(name = "AmenityName", nullable = false, unique = true)
	    private String amenityName;

	    @Column(name = "Description")
	    private String description;

	    @ManyToMany(mappedBy = "amenities")
	    private Set<Turf> turfs;

		public Integer getAmenityId() {
			return amenityId;
		}

		public void setAmenityId(Integer amenityId) {
			this.amenityId = amenityId;
		}

		public String getAmenityName() {
			return amenityName;
		}

		public void setAmenityName(String amenityName) {
			this.amenityName = amenityName;
		}

		public String getDescription() {
			return description;
		}

		public void setDescription(String description) {
			this.description = description;
		}

		public Set<Turf> getTurfs() {
			return turfs;
		}

		public void setTurfs(Set<Turf> turfs) {
			this.turfs = turfs;
		}

	   
	}


