# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
  # 1. Вибираємо операційну систему (Linux Ubuntu 22.04)
  # Це виконання Stage 3: deploy on different operating systems [cite: 37]
  config.vm.box = "ubuntu/jammy64"

  # 2. Налаштування ресурсів віртуалки (щоб не гальмувала)
  config.vm.provider "virtualbox" do |vb|
    vb.memory = "2048"
    vb.cpus = 2
  end

  # 3. Скрипт налаштування (Provisioning)
  # Тут ми автоматизуємо встановлення середовища
  config.vm.provision "shell", inline: <<-SHELL
    echo "=== Start Provisioning ==="
    
    # Оновлюємо списки пакетів
    sudo apt-get update
    sudo apt-get install -y git wget

    # Встановлюємо .NET 8 SDK (бо на "голому" Linux його немає)
    echo "=== Installing .NET 8 SDK ==="
    wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update
    sudo apt-get install -y dotnet-sdk-8.0

    # Клонуємо твій репозиторій з GitHub
    # Це відповідає пораді (Tip): download repository and build/run it 
    echo "=== Cloning Repository ==="
    if [ -d "/home/vagrant/ConferenceManager" ]; then
        rm -rf /home/vagrant/ConferenceManager
    fi
    git clone https://github.com/kiborg-sashko/ConferenceManager.git /home/vagrant/ConferenceManager

    # Запускаємо програму
    echo "=== Running ConferenceManager ==="
    cd /home/vagrant/ConferenceManager
    dotnet run --project ConferenceManager.ConsoleApp
    
    echo "=== Done! ==="
  SHELL
end