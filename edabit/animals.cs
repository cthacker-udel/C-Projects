using System;

namespace Animals {

    class Dog {

        private string name;

        public Dog() {

        }

        public Dog(string newName) {

        }

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }

        public Dog Bark() {
            Console.WriteLine("bark bark");
            return this;
        }


    }

    public class Cat {

        private string name;
        public Cat() {

        }

        public Cat(string newName) {
            this.name = newName;
        }

        public string Name {

            get {
                return this.name;
            }
            set {
                this.name = value;
            }

        }


    }




};