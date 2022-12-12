﻿using Domain.Guests.Exceptions;
using Domain.Guests.Ports;
using Domain.Guests.ValueObjects;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }


        private void ValidateState()
        {
            if (DocumentId == null || string.IsNullOrEmpty(DocumentId.IdNumber) || DocumentId.IdNumber.Length <= 3 || DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentException();
            }

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email))
            {
                throw new MissingRequiredInformationException();
            }
            if (!Utils.ValidateEmail(this.Email))
            {
                throw new InvalidEmailException();

            }
        }
        public async Task Save(IGuestRepository guestRepository)
        {

            this.ValidateState();
            if (this.Id == 0)
            {
                await guestRepository.Create(this);
            }
            else
            {
                //await guestRepository.Update(this);
            }

        }

    }
}
