# 🚀 Semantic Kernel Strapi AI Agent Orchestrator

Bu proje, **Semantic Kernel** kullanarak **Strapi CMS** ile Yapay Zeka modelleri (**Google Gemini**, **OpenAI**) arasında akıllı bir köprü kuran bir otonom ajan (agent) uygulamasıdır. 

Sistem, stok seviyelerini otonom olarak denetler, düşük stoklu ürünleri analiz eder ve bu verileri kullanarak otomatik kampanya içerikleri oluşturup tekrar Strapi üzerine kaydeder.


## Demo

<img width="1019" height="568" alt="image" src="https://github.com/user-attachments/assets/cef9f003-00b5-4e73-8d95-f92c8aab95e7" />

<img width="1021" height="403" alt="image" src="https://github.com/user-attachments/assets/317314b5-98c0-4af4-a471-7c29b67816e7" />


## --- 

## 🛠 Teknik Yetenekler (Tech Stack)

* **Orchestration:** [Microsoft Semantic Kernel](https://github.com/microsoft/semantic-kernel)
* **LLM (Brain):** Google Gemini / OpenAI 
* **CMS & Backend:** [Strapi v5](https://strapi.io/)
* **Runtime:** .NET 10
* **Pattern:** Agentic Workflow & Tool Calling (Plugins)

## 🏗️ Mimari Yapı

Proje, LLM'in dış dünya ile konuşmasını sağlayan **Native Function** yapısı üzerine kuruludur:

1.  **Stok Denetimi:** Ajan, `StrapiPlugin` üzerinden stok verilerini çeker.
2.  **Karar Mekanizması:** Gemini, hangi ürünlerin kritik seviyede (örn: <10) olduğunu analiz eder.
3.  **Yaratıcı Süreç:** Belirlenen ürünler için "Stokta Son X Adet!" temalı pazarlama metinleri oluşturur.
4.  **Aksiyon:** Oluşturulan içerikleri Strapi API'sine otomatik olarak taslak (draft) olarak kaydeder.

## 💻 Kurulum

### 1. Gereksinimler
* .NET SDK (v10)
* Çalışan bir Strapi örneği
* Google AI Studio üzerinden alınmış bir API Key

### 2. NuGet Paketleri
Projenin çalışması için gerekli temel paketler:
```bash
dotnet add package Microsoft.SemanticKernel
dotnet add package Microsoft.SemanticKernel.Connectors.Google --prerelease

```


