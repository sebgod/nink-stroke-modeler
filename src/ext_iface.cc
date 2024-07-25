#include <vector>
#include "ink_stroke_modeler/stroke_modeler.h"
#include "ink_stroke_modeler/types.h"

#ifdef _WIN32
#    define LIBRARY_API __declspec(dllexport)
#else
#    define LIBRARY_API
#endif

extern "C" {
    typedef intptr_t ResultListHandle;

    LIBRARY_API ink::stroke_model::StrokeModeler* nink_init_modeler(
        const ink::stroke_model::WobbleSmootherParams wobbleParams,
        const ink::stroke_model::PositionModelerParams positionModelerParams,
        const ink::stroke_model::SamplingParams samplingParams,
        const ink::stroke_model::StylusStateModelerParams stylusStateModelerParams
    )
    {
        auto modeler = new ink::stroke_model::StrokeModeler;
        ink::stroke_model::StrokeModelParams params {
            .wobble_smoother_params = wobbleParams,
            .position_modeler_params = positionModelerParams,
            .sampling_params = samplingParams,
            .stylus_state_modeler_params = stylusStateModelerParams,
            .prediction_params = ink::stroke_model::StrokeEndPredictorParams{}
        };

        if (modeler->Reset(params).ok()) {
            return modeler;
        } else {
            delete modeler;

            return nullptr;
        }
    }

    LIBRARY_API bool nink_update(
        ink::stroke_model::StrokeModeler* modeler,
        ink::stroke_model::Input::EventType eventType,
        ink::stroke_model::Vec2 position,
        double time,
        float pressure,
        ResultListHandle* resultListHandle,
        ink::stroke_model::Result** results,
        int32_t* resultsCount
    )
    {
        ink::stroke_model::Input input = { 
            .event_type{ eventType },
            .position{ position },
            .time{ ink::stroke_model::Time(time) },
            .pressure = pressure
        };
        auto resultsVec = new std::vector<ink::stroke_model::Result>();
        *resultListHandle = reinterpret_cast<ResultListHandle>(resultsVec);

        if (modeler->Update(input, *resultsVec).ok()) {
            *results = resultsVec->data();
            *resultsCount = resultsVec->size();

            return true;
        } else {
            *results = nullptr;
            *resultsCount = 0;

            return false;
        }
    }

    LIBRARY_API bool nink_release_results_handle(ResultListHandle hItems)
    {
        auto items = reinterpret_cast<std::vector<ink::stroke_model::Result>*>(hItems);
        delete items;

        return true;
    }

    LIBRARY_API void nink_free_modeler(ink::stroke_model::StrokeModeler* modeler)
    {
        delete modeler;
    }
}