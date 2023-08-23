
import time
from controllers.mainProcessAnalytics import mainProcessAnalytics
from fillingDatabase.fillDatabase import fillDatabase


def main():
  fillDatabase()
  print('\nEmpeazando proceso\n')
  mainProcessAnalytics()
    


if __name__ == "__main__":
  main()
